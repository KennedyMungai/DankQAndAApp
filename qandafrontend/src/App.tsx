import Header from './Header';
import HomePage from './HomePage';
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
