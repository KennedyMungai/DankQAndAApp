import React from 'react'
import { QuestionData } from './QuestionsData'

interface Props {
    data: QuestionData;
}

const Question = ({ data }: Props) => {
  return (
    <div>
        <div>
            {data.title}
        </div>
        <div>
            {
                data.content.length > 50
                ? `${data.content.substring(0, 50)}...`
                : data.content
            }
        </div>
        <div>
            {
                `Asked by ${data.userName} on
                ${data.created.toLocaleDateString()}
                ${data.created.toLocaleTimeString()}`
            }
        </div>
    </div>
  )
}

export default Question