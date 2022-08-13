import { QuestionData } from "./QuestionsData";

interface QuestionsState {
    readonly loading: boolean;
    readonly unanswered: QuestionData[];
    readonly viewing: QuestionData | null;
    readonly searched: QuestionData[];
}

export interface AppState {
    readonly questions: QuestionsState;
}

const initialQuestionState: QuestionsState = {
    loading: false,
    unanswered: [],
    viewing: null,
    searched: []
};

export const GETTINGUNANSWEREDQUESTIONS = 'GettingUnansweredQuestions';

export const gettingUnansweredQuestionsAction = () => (
    {
        type: GETTINGUNANSWEREDQUESTIONS,
     } as const
);

export const GOTUNANSWEREDQUESTIONS = 'GotUnansweredQuestions';

export const gotUnansweredQuestionsAction = (questions: QuestionData[]) => (
    {
        type: GOTUNANSWEREDQUESTIONS,
        questions: questions
    } as const
);

export const GETTINGQUESTION = 'GettingQuestion';