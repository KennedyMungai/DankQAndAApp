/** @jsxImportSource @emotion/react */
import { css } from '@emotion/react'
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import AskPage from './AskPage';

import Header from './Header';
import HomePage from './HomePage';
import SearchPage from './SearchPage';
import SignInPage from './SignInPage';
import { fontFamily, fontSize, gray2 } from './Styles'

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
        <HomePage />
      </div>
    </BrowserRouter>
  );
}

export default App;
