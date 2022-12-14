/** @jsxImportSource @emotion/react */
import { css } from '@emotion/react'
import React, { Suspense } from 'react';
import { Provider } from 'react-redux';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { AuthProvider } from './Auth';
import AuthorizedPage from './AuthorizedPage';
// import AskPage from './AskPage';
import Header from './Header';
import HomePage from './HomePage';
import NotFoundPage from './NotFoundPage';
import QuestionPage from './QuestionPage';
import SearchPage from './SearchPage';
import SignInPage from './SignInPage';
import  SignOutPage  from './SignOutPage';
import { configureStore } from './Store';
import { fontFamily, fontSize, gray2 } from './Styles'

const AskPage = React.lazy(() => import('./AskPage'));

const store = configureStore();

function App() {
  <AuthProvider>
      return (
        <Provider store={store}>
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
    
                <Route path="ask" element={
                  <Suspense fallback={
                    <div css={
                      css`
                        margin-top: 100px;
                        text-align: center;
                      `
                    }>
                      Loading...
                    </div>
                  }>
                    <AuthorizedPage>
                      <AskPage />
                    </AuthorizedPage>
                  </Suspense>
                } />
    
                <Route path="signin" element={<SignInPage action="signin" />} />
                <Route path='/signin-callback' element={<SignInPage action="signin-callback" />} />
                <Route path='signout' element={<SignOutPage action="signout" />} />
                <Route path='/signout-callback' element={ <SignOutPage action="signout-callback" /> } />
                <Route path="*" element={<NotFoundPage/>} />
                <Route path="questions/:questionId" element={<QuestionPage />} />
              </Routes>
            </div>
          </BrowserRouter>
        </Provider>
      );
  </AuthProvider>
}

export default App;
