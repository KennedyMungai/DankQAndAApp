/** @jsxImportSource @emotion/react */
import { css } from '@emotion/react'
import React, { useState } from 'react'
import { useSearchParams } from 'react-router-dom'
import { Page } from './Page'
import QuestionList from './QuestionList'
import { searchQuestions, QuestionData } from './QuestionsData'

const SearchPage = () => {
  const [searchParams] = useSearchParams();
  const [questions, setQuestions] = useState<QuestionData[]>([]);

  const search = searchParams.get('criteria') || "";

  return (
    <Page title='Search Results'>{null}</Page>
  )
}

export default SearchPage