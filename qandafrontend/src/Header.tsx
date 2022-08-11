import React from 'react'
import UserIcon from './Icon'

const Header = () => {
    return (
        <>
            <a href="./">Q & A</a>
            <input type="text" name="" id="" placeholder="Search..." />
            <a href="./signin">
                <UserIcon />
                <span>Sign In</span>
            </a>
        </>
    )
}

export default Header