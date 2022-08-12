/** @jsxImportSource @emotion/react */
import { css } from '@emotion/react'

import Header from './Header';
import HomePage from './HomePage';

function App() {
  return (
    <div className={styles.container}>
      <Header />
      <HomePage />
    </div>
  );
}

export default App;
