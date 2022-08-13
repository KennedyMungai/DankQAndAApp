/** @jsxImportSource @emotion/react */
import { css } from '@emotion/react'
import { PrimaryButton } from "./Styles";
import React, { useEffect, useState } from 'react'
import { Page } from './Page'
import PageTitle from './PageTitle'
import QuestionList from './QuestionList'
import { getUnansweredQuestions } from './QuestionsData'
import { useNavigate } from 'react-router-dom';
import { useSelector, useDispatch } from 'react-redux';
import { 
          gettingUnansweredQuestionsAction, 
          gotUnansweredQuestionsAction, 
          AppState 
        } from './Store';

const HomePage = () => {
  // const [questionsLoading, setQuestionsLoading] = useState(true);
  const dispatch = useDispatch();

  const questions = useSelector(
    (state: AppState) => state.questions.unanswered
  );

  const questionsLoading = useSelector(
    (state: AppState) => state.questions.loading
  );

  useEffect(() => {
    const doGetUnansweredQuestions = async () => {
      dispatch(gettingUnansweredQuestionsAction());
      const unansweredQuestions = await getUnansweredQuestions();
      dispatch(gotUnansweredQuestionsAction(unansweredQuestions));
    };
    
    doGetUnansweredQuestions();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const navigate = useNavigate();
  
  const handleAskQuestionClick = () => {
    navigate('ask');
  };

  return (
    <Page>
      <div
        css={
          css`
            display: flex;
            align-items: center;
            justify-content: space-between;
          `
        }
      >
        <PageTitle>Unanswered Questions</PageTitle>
        <PrimaryButton onClick={handleAskQuestionClick} >Ask A Question</PrimaryButton>
      </div>

      {questionsLoading ? (
        <div>Loading...</div>
      ) : (
        <QuestionList data={questions} renderItem={(question) => <div>{question.title}</div>} />
      )}
    </Page>
  )
}

export default HomePage