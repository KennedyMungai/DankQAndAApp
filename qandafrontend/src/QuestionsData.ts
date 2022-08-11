export interface QuestionData {
    questionId: number;
    title: string;
    content: string;
    username: string;
    create: Date;
}

export interface AnswerData {
    answerId: number;
    content: string;
    username: string;
    created: Date;
}