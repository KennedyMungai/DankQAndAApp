/** jsxImportSource @emotion/react */
import { css } from '@emotion/react'

import React from 'react'
import PageTitle from './PageTitle'

interface Props {
    title?: string;
    children: React.ReactNode;
}

const Page = ({title, children}: Props) => {
  return (
    <div>
        {title && <PageTitle>{title}</PageTitle>}
        {children}
    </div>
  )
}

export default Page