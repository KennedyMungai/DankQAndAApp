/** @jsxImportSource @emotion/react */
import { css } from '@emotion/react'
import React from 'react'
import { useSearchParams } from 'react-router-dom'
import { Page } from './Page'
import QuestionList from './QuestionList'
import { searchQuestions, QuestionData } from './QuestionsData'

const SearchPage = () => {
  const [searchParams] = useSearchParams();

  return (
    <Page>Question Page</Page>
  )
}

export default SearchPage