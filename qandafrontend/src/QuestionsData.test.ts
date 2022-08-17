import { mapQuestionFromServer } from './QuestionsData';

test('When mapQuestionFromServer is called with the question, created should be turned into a date', () => { 
    const result = mapQuestionFromServer({
        questionId: 1,
        title: 'test',
        content: 'test',
        userName: 'test',
        created: '2022-08-01T00:00:00.000z',
        answers: []
    });

    expect(result).toEqual({
        questionId: 1,
        title: 'test',
        content: 'test',
        userName: 'test',
        created: new Date(Date.UTC(2021,0, 1, 0, 0, 0, 0)),
        answers:[]
    });
 });