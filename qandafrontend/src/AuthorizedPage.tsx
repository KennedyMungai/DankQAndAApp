import React from 'react'
import { useAuth } from './Auth';
import {Page} from './Page'

const AuthorizedPage: React.FC = (children) => {
    const { isAuthenticated } = useAuth();

    if(isAuthenticated)
    {
        return <>
            {children}
        </>
    }
    else
    {
        return(
            <Page title='You do not have access to this page'>
                {null}
            </Page>
        );
    }

    return (
        <div>AuthorizedPage</div>
    )
}

export default AuthorizedPage