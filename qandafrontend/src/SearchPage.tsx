/** @jsxImportSource @emotion/react */
import { css } from '@emotion/react'
import React, { useEffect } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { useSearchParams } from 'react-router-dom'
import { Page } from './Page'
import QuestionList from './QuestionList'
import { searchQuestions } from './QuestionsData'
import { 
          AppState,
          searchingQuestionsAction,
          searchedQuestionsAction
        } from './Store';

const SearchPage = () => {
  const [searchParams] = useSearchParams();
  const dispatch = useDispatch();
  const questions = useSelector(
    (state: AppState) => state.questions.searched
  );

  const search = searchParams.get('criteria') || "";

  useEffect(() => {
    const doSearch = async (criteria: string) => {
      dispatch(searchingQuestionsAction());
      const foundResults = await searchQuestions(criteria);
      dispatch(searchedQuestionsAction(foundResults));
    };

    doSearch(search)
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [search])
  

  return (
    <Page title='Search Results'>
      {
        search && (
          <p css={css`
            font-size: 16px;
            font-style: italic;
            margin-top: 0px;
          `}>
            for "search"
          </p>
        )
      }

      <QuestionList data={questions} />
    </Page>
  )
}

export default SearchPage