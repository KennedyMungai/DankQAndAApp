/** @jsxImportSource @emotion/react */
import { css } from '@emotion/react'
import { fontFamily, fontSize, gray1, gray2, gray5 } from './Styles'
import React, { useState } from 'react'
import UserIcon from './Icon'
import { Link, useSearchParams, useNavigate } from 'react-router-dom'
import { useForm } from 'react-hook-form'

type FormData = {
    search: string;
};

const Header = () => {
    const [searchParams] = useSearchParams();
    const criteria = searchParams.get('criteria') || '';
    const { register, handleSubmit } = useForm<FormData>();
    const navigate = useNavigate();

    // const handleSearchInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    //     searchState(e.currentTarget.value);
    // };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
    };

    return (
        <div css={css`
            position: fixed;
            top: 0;
            width: 100%;
            display: flex;
            align-items: center;
            justify-content: space-between;
            padding: 10px 20px;
            background-color: #fff;
            border-bottom: 1px solid ${gray5};
            box-shadow: 0 3px 7px 0 rgba(110, 112, 114, 0.21);
            `
        }>
            <Link to="/" css={css`
                font-size: 24px;
                font-weight: bold;
                color: ${gray1};
                text-decoration: none;
            `}>Q & A</Link>

            <form onSubmit={handleSubmit(submitForm)}>
                <input 
                    ref={register}
                    name="search"
                    type="text" 
                    defaultValue={criteria}
                    placeholder="Search..." 
                    css={css`
                        box-sizing: border-box;
                        font-family: ${fontFamily};
                        font-size: ${fontSize};
                        padding: 8px 10px;
                        border-radius: 3px;
                        color: ${gray2};
                        background-color: white ;
                        width: 200px ;
                        height: 30px;

                        :focus {
                            outline-color: ${gray5};
                        }
                    `}
                    />
            </form>

            <Link 
                to="signin"
                css={
                    css`
                        font-family: ${fontFamily};
                        font-size: ${fontSize};
                        padding: 5px 10px;
                        background-color: transparent ;
                        color: ${gray2};
                        text-decoration: none;
                        cursor: pointer;

                        :focus {
                            outline-color: ${gray5};
                        }

                        span {
                            margin-left: 7px;
                        }
                    `
                }
                >
                <UserIcon />
                <span>Sign In</span>
            </Link>
        </div>
    )
}

export default Header