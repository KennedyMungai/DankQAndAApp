import React, { createContext, useContext } from "react";
import createAuth0Client from "@auth0/auth0-spa-js";
import Auth0Client from "@auth0/auth0-spa-js/dist/typings/Auth0Client";
import { authSettings } from './AppSettings';

interface Auth0User {
    name: string;
    email: string;
}

interface IAuth0Context {
    isAuthenticated: boolean;
    user?: Auth0User;
    signIn: () => void;
    signOut: () => void;
    loading: boolean;
}

export const Auth0Context = createContext<IAuth0Context>({
    isAuthenticated: false,
    signIn: () => {},
    signOut: () => {},
    loading: true
});

export const useAuth = () => useContext(Auth0Context);