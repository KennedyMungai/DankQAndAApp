import Header from './Header';
import HomePage from './HomePage';

/** @jsxImportSource @emotion/react */
import { css } from '@emotion/react'

function App() {
  return (
    <div className={styles.container}>
      <Header />
      <HomePage />
    </div>
  );
}

export default App;
