import React, { useEffect } from 'react'
import Page from './Page'
import PageTitle from './PageTitle'
import QuestionList from './QuestionList'
import { getUnansweredQuestions } from './QuestionsData'

const HomePage = () => {
  useEffect(() => {
    console.log('first rendered')
  }, []);

  return (
    <Page>
      <div>
        <PageTitle>Unanswered Questions</PageTitle>
        <button>Ask A Question</button>
      </div>
      {/* <QuestionList data={getUnansweredQuestions()} renderItem={(question) => <div>{question.title}</div>} /> */}
    </Page>
  )
}

export default HomePage