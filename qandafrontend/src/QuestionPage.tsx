/** @jsxImportSource @emotion/react */

import { css } from '@emotion/react'
import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { Page } from './Page'
import { getQuestion, QuestionData } from './QuestionsData'

const QuestionPage = () => {
    const [question, setQuestion] = useState<QuestionData | null>(null)
    const { questionId } = useParams();

    useEffect(() => {
      const doGetQuestion = async (questionId: number) => {
        const foundQuestion = await getQuestion(questionId);
        setQuestion(foundQuestion);
      }
    
      if (questionId) {
        doGetQuestion(Number(questionId));
      }
    }, [questionId])

  return (
    <Page>Question Page {questionId}</Page>
  )
}

export default QuestionPage