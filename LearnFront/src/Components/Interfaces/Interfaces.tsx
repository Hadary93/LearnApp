export interface IArticle{
    id:number;
    name:string;
    paragraphs : Array<IParagraph>;
}
export interface IParagraph{
    id:number;
    sentences : Array<ISentence>;
}
export interface ISentence {
    key?: number,
    id?: number,
    hash?: string,
    words?: IWord[],
    difficulty?: number,
    sentenceCount?: number
  }
export interface IWord {
    key?: number,
    id?: number,
    wordText?: string,
    translation?: string,
    favourite?: boolean,
    difficulty?: number,
    wordCount?: number,
    group?: string
  }
  