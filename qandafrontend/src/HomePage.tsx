/** @jsxImportSource @emotion/react */
import { css } from '@emotion/react'

import { PrimaryButton } from "./Styles";

import React, { useEffect, useState } from 'react'
import Page from './Page'
import PageTitle from './PageTitle'
import QuestionList from './QuestionList'
import { getUnansweredQuestions, QuestionData } from './QuestionsData'

const HomePage = () => {
  const [questions, setQuestions] = useState<QuestionData[]>([]);
  const [questionsLoading, setQuestionsLoading] = useState(true);

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
        <button onClick={handleAskQuestionClick} >Ask A Question</button>
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