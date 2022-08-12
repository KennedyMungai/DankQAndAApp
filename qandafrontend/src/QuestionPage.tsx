/** @jsxImportSource @emotion/react */

import { css } from '@emotion/react'
import React, { useState } from 'react'
import { useParams } from 'react-router-dom'
import { Page } from './Page'
import { QuestionData } from './QuestionsData'

const QuestionPage = () => {
    const [question, setQuestion] = useState<QuestionData | null>(null)

    const { questionId } = useParams();

  return (
    <Page>Question Page {questionId}</Page>
  )
}

export default QuestionPage