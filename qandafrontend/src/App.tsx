/** @jsxImportSource @emotion/react */
import { css } from '@emotion/react'

import Header from './Header';
import HomePage from './HomePage';

function App() {
  return (
    <div css={
      css`
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        font-size: 16px;
        color: #5c5a5a;
      `
    }>
      <Header />
      <HomePage />
    </div>
  );
}

export default App;
