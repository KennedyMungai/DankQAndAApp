import React from 'react'
import { useParams } from 'react-router-dom'
import { Page } from './Page'

const QuestionPage = () => {
    const { questionId } = useParams();

  return (
    <Page>Question Page</Page>
  )
}

export default QuestionPage