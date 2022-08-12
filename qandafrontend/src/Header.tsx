/** @jsxImportSource @emotion/react */
import { css } from '@emotion/react'
import { fontFamily, fontSize, gray1, gray2, gray3 } from './Styles'

import React from 'react'
import UserIcon from './Icon'

const Header = () => {
    const handleSearchInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        console.log(e.currentTarget.value);
    };

    return (
        <div className={styles.container}>
            <a href="./">Q & A</a>
            <input type="text" name="" id="" placeholder="Search..." onChange={handleSearchInputChange} />
            <a href="./signin">
                <UserIcon />
                <span>Sign In</span>
            </a>
        </div>
    )
}

export default Header