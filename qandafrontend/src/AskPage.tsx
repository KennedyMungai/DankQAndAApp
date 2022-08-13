import React from 'react'
import { useForm } from 'react-hook-form';
import { Page } from './Page'
import { 
    FieldSet, 
    FieldContainer, 
    FieldLabel, 
    FieldInput, 
    FieldTextArea, 
    FormButtonContainer, 
    PrimaryButton } from './Styles'


type FormData = {
  title: string;
  content: string;
};

const AskPage = () => {
  const { register } = useForm<FormData>();

  return (
    <Page title='Ask A Question'>
      <form>
        <FieldSet>
        </FieldSet>
      </form>
    </Page>
  )
}

export default AskPage