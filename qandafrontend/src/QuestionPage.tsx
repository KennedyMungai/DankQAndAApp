/** @jsxImportSource @emotion/react */
import { css } from '@emotion/react'
import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { Page } from './Page'
import { getQuestion, QuestionData } from './QuestionsData'
import { gray6 } from './Styles'

const QuestionPage = () => {
    const [question, setQuestion] = useState<QuestionData | null>(null)
    const { questionId } = useParams();

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
                </>
            )}
        </div>
    </Page>
  )
}

export default QuestionPage