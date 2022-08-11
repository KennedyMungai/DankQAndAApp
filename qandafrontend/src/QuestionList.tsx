import React from 'react'
import { QuestionData } from './QuestionsData'

interface Props {
    data: QuestionData[];
}

const QuestionList = ({ data }: Props) => {
  return (
    <ul>
        {data.map((question) => (
            <li key={question.questionId}></li>
        ))}
    </ul>
  )
}

export default QuestionList