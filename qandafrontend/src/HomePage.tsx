/** @jsxImportSource @emotion/react */
import { css } from '@emotion/react'

import { PrimaryButton } from "./Styles";

import React, { useEffect, useState } from 'react'
import { Page } from './Page'
import PageTitle from './PageTitle'
import QuestionList from './QuestionList'
import { getUnansweredQuestions, QuestionData } from './QuestionsData'
import { useNavigate } from 'react-router-dom';

const HomePage = () => {
  const [questions, setQuestions] = useState<QuestionData[]>([]);
  const [questionsLoading, setQuestionsLoading] = useState(true);

  const navigate = useNavigate();

  useEffect(() => {
    const doGetUnansweredQuestions =async () => {
      const unansweredQuestions = await getUnansweredQuestions();
      setQuestions(unansweredQuestions);
      setQuestionsLoading(false);
    };

    doGetUnansweredQuestions();
  }, []);

  const handleAskQuestionClick = () => {
    console.log('TODO - Move to the ask page');
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