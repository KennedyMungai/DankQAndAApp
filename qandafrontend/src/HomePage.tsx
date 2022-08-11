import React from 'react'
import QuestionList from './QuestionList'
import { getUnansweredQuestions } from './QuestionsData'

const HomePage = () => {
  return (
    <div>
        <div>
            <h2>Unanswered Questions</h2>
            <button>Ask a question</button>
            <QuestionList data={getUnansweredQuestions()} />
        </div>
    </div>
  )
}

export default HomePage