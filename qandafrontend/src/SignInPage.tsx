import React from 'react'
import { Page } from './Page'
import { StatusText } from './Styles'
import { useAuth } from './Auth'

type SigninAction = 'signin' | 'signin-callback';

interface Props {
  action: SigninAction;
}

const SignInPage = ( {action}: Props ) => {
  return (
    <Page title='Sign In'>{null}</Page>
  )
}

export default SignInPage