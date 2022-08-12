/** @jsxImportSource @emotion/react */
import { css } from '@emotion/react'
import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
// import AskPage from './AskPage';
import Header from './Header';
import HomePage from './HomePage';
import NotFoundPage from './NotFoundPage';
import QuestionPage from './QuestionPage';
import SearchPage from './SearchPage';
import SignInPage from './SignInPage';
import { fontFamily, fontSize, gray2 } from './Styles'

const AskPage = React.lazy(() => import('./AskPage'));

function App() {
  return (
    <BrowserRouter>
      <div css={
        css`
          font-family: ${fontFamily};
          font-size: ${fontSize};
          color: ${gray2};
        `
      }>
        <Header />
        <Routes>
          <Route path="" element={<HomePage/>} />
          <Route path="search" element={<SearchPage/>} />
          <Route path="ask" element={<AskPage/>} />
          <Route path="signin" element={<SignInPage/>} />
          <Route path="*" element={<NotFoundPage/>} />
          <Route path="questions/:questionId" element={<QuestionPage />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
