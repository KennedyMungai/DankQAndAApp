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
    PrimaryButton,
    FieldError } from './Styles'


type FormData = {
  title: string;
  content: string;
};

const AskPage = () => {
  const { register, errors } = useForm<FormData>({
    mode: 'onBlur'
  });

  return (
    <Page title='Ask A Question'>
      <form>
        <FieldSet>
          <FieldContainer>
            <FieldLabel htmlFor='title'>
              Title
            </FieldLabel>
            <FieldInput
              id='title'
              name='title'
              type="text"
              ref={register({
                required: true,
                minLength: 10
              })}
            />

              {
                errors.title && 
                  errors.title.type === 'required' && (
                    <FieldError>
                      You must enter the question title
                    </FieldError>
                  )
              }

              {
                errors.title && 
                  errors.title.type === 
                    'minLength' && (
                      <FieldError>
                        The title must be at leadst 10 characters
                      </FieldError>
                    )
              }

          </FieldContainer>
          <FieldContainer>
            <FieldLabel htmlFor='content'>
              Content
            </FieldLabel>
            <FieldTextArea 
              id='content'
              name='content'
              ref={register({
                required: true,
                minLength: 50
              })}
            />
          </FieldContainer>
          <FormButtonContainer>
            <PrimaryButton type='submit'>
              Submit Your Question
            </PrimaryButton>
          </FormButtonContainer>
        </FieldSet>
      </form>
    </Page>
  )
}

export default AskPage