import React from 'react'
import UserIcon from './Icon'
import './Header.css'

const Header = () => {
    const handleSearchInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        console.log(e.currentTarget.value);
    };

    return (
        <>
            <a href="./">Q & A</a>
            <input type="text" name="" id="" placeholder="Search..." onChange={handleSearchInputChange} />
            <a href="./signin">
                <UserIcon />
                <span>Sign In</span>
            </a>
        </>
    )
}

export default Header