/** @jsxImportSource @emotion/react */
import { css } from '@emotion/react'

import React from 'react'
import user from './user.svg'

const UserIcon = () => {
  return (
    <img 
      src={user} 
      alt="User" 
      width="12px"
      css={
        css`
          width: 12px;
          opacity: 0.6;
        `
      }
      />
  )
}

export default UserIcon