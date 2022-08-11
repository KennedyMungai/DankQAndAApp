import React from 'react'
import { QuestionData } from './QuestionsData'

interface Props {
    data: QuestionData[];
}

const QuestionList = (props: Props) => {
  return (
    <ul>
        {props.data.map((question) => (
            <li key={question.questionId}></li>
        ))}
    </ul>
  )
}

export default QuestionList