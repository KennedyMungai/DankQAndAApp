/** @jsxImportSource @emotion/react */
import { css } from '@emotion/react';
import React from 'react'
import { QuestionData } from './QuestionsData'
import { gray2, gray3 } from './Styles';

interface Props {
    data: QuestionData;
    showContent?: boolean;
}

const Question = ({ data, showContent = true }: Props) => {
  return (
    <div
        css={
            css`
                padding: 10px 0px;
            `
        }
    >
        <div
            css={
                css`
                    padding: 10px 0px;
                    font-size: 19px;
                `
            }
        >
            {data.title}
        </div>
        <div>
            {
                showContent &&
                (
                    data.content.length > 50
                    ? `${data.content.substring(0, 50)}...`
                    : data.content
                )
            }
        </div>
        <div>
            {
                `Asked by ${data.userName} on
                ${data.created.toLocaleDateString()}
                ${data.created.toLocaleTimeString()}`
            }
        </div>
    </div>
  )
}

export default Question