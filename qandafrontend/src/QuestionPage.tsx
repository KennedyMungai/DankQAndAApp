/** @jsxImportSource @emotion/react */
import { css } from '@emotion/react'
import React, { useEffect, useState } from 'react'
import { useForm } from 'react-hook-form'
import { useSelector, useDispatch } from 'react-redux'
import { useParams } from 'react-router-dom'
import AnswerList from './AnswerList'
import { Page } from './Page'
import { 
        AppState, 
        gettingQuestionAction, 
        gotQuestionAction 
    } from './Store';
import { 
        getQuestion, 
        PostAnswer, 
    } from './QuestionsData'
import { 
        gray3, 
        gray6, 
        FieldSet, 
        FieldContainer, 
        FieldLabel, 
        FieldTextArea, 
        FormButtonContainer, 
        PrimaryButton ,
        FieldError,
        SubmissionSuccess
    } from './Styles'

type FormData = {
    content: string;
};

const QuestionPage = () => {
    const { questionId } = useParams();
    const { 
            register, 
            errors,
            handleSubmit,
            formState 
        } = useForm<FormData>({
        mode: 'onBlur'
    });

    const [successfullySubmitted, setSuccessfullySubmitted] = useState(false);
    const dispatch = useDispatch();

    const question = useSelector(
        (state: AppState) => state.questions.viewing
    );

    const submitForm = async (data: FormData) => {
        const result = await PostAnswer({
            questionId: question!.questionId,
            content: data.content,
            userName: "Fred",
            created: new Date()
        });

        setSuccessfullySubmitted(result ? true : false);
    };

    useEffect(() => {
        const doGetQuestion = async (questionId: number) => {
            const foundQuestion = await getQuestion(questionId);
            setQuestion(foundQuestion);
        }
    
        if (questionId) {
            doGetQuestion(Number(questionId));
        }
    }, [questionId])

    return (
        <Page>
            <div css={
                css`
                    background-color: white;
                    padding: 15px 20px 20px 20px;
                    border-radius: 4px;
                    border: 1px solid ${gray6};
                    box-shadow: 0 3px 5px 0 rgba(0, 0, 0, 0.16);
                `
            }>
                <div css={
                    css`
                        font-size: 19px;
                        font-weight: bold;
                        margin: 10px 0px 5px;
                    `
                }>
                    {question === null ? '' : question.title}
                </div>
                {question !== null && (
                    <>
                        <p css={
                            css`
                                margin-top: 0px;
                                background-color: white;
                            `
                        }>
                            {question.content}
                        </p>

                        <div css={
                            css`
                                font-size: 12px;
                                font-style: italic;
                                color: ${gray3};
                            `
                        }>
                            {
                                `Asked by ${question.userName} on 
                                ${question.created.toLocaleDateString()} 
                                ${question.created.toLocaleTimeString()}`
                            }
                        </div>
                        <AnswerList data={question.answers} />

                        <form css={
                            css`
                                margin-top: 20px;
                            `
                        } onSubmit={handleSubmit(submitForm)}>
                            <FieldSet
                                disabled={
                                    formState.isSubmitting || successfullySubmitted
                                }
                            >
                                <FieldContainer>
                                    <FieldLabel htmlFor='content'>
                                        Your Answer
                                    </FieldLabel>
                                    <FieldTextArea
                                        id='content'
                                        name='content'
                                        ref={register({
                                            required: true,
                                            minLength: 50
                                        })}
                                        />

                                        {
                                            errors.content && 
                                                errors.content.type === 'required' && (
                                                    <FieldError>
                                                        You muest enter the answer
                                                    </FieldError>
                                                )
                                        }

                                        {
                                            errors.content && 
                                                errors.content.type === 'minLength' && (
                                                    <FieldError>
                                                        The answer must be at least 50 characters
                                                    </FieldError>
                                                )
                                        }

                                </FieldContainer>
                                <FormButtonContainer>
                                    <PrimaryButton type='submit'>
                                        Submit Your Answer
                                    </PrimaryButton>
                                </FormButtonContainer>

                                {
                                    successfullySubmitted && (
                                        <SubmissionSuccess>
                                            Your answer was successfully submitted
                                        </SubmissionSuccess>
                                    )
                                }

                            </FieldSet>
                        </form>
                    </>
                )}
            </div>
        </Page>
    )
}

export default QuestionPage