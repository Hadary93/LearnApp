import { IArticle, IWord,ISentence } from "../Interfaces/Interfaces";

const textAPI: string = "https://localhost:7194";
export const textWordAPI = `${textAPI}/word`
const textSentenceAPI:string = `${textAPI}/sentence`;
const textArticleAPI = `${textAPI}/article`
const WordByText = (word: string) => `${textWordAPI}/GetWord/${word}`;

//#region Word
export async function GetAllWords(): Promise<IWord[] | undefined> {
  try {
    const response = await fetch(`${textWordAPI}`);

    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    return await response.json() as IWord[];
  } catch (error) {
    console.error("Error adding word:", error);
  }
}
export async function GetWordByText(word: string): Promise<IWord | undefined> {
  try {
    const response = await fetch(`${WordByText(word)}`);

    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    return await response.json() as IWord;
  } catch (error) {
    console.error("Error adding word:", error);
  }
}
export async function FavouriteWord(word: string | null): Promise<boolean | undefined> {
  try {
    const response = await fetch(`${textWordAPI}/favourite/${word}`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      }
    });

    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    return true;
  } catch (error) {
    console.error("Error updating word:", error);
  }
}
export async function UpdateWord(updatedWord: IWord | null, setWord: React.Dispatch<React.SetStateAction<IWord | null>> | null): Promise<boolean | undefined> {
  try {
    const response = await fetch(`https://localhost:7194/Word/`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(updatedWord)
    });

    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    if (setWord) setWord(updatedWord)
    return true;
  } catch (error) {
    console.error("Error updating word:", error);
  }
}
export async function AddWord(newWord: IWord | null): Promise<boolean | undefined> {
  try {
    const response = await fetch(`${textWordAPI}`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(newWord)
    });

    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    return true;
  } catch (error) {
    console.error("Error adding word:", error);
  }
}
export async function GetFavourites(): Promise<IWord[] | undefined> {
  try {
    const response = await fetch(`${textWordAPI}/get-favourites`);

    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    return await response.json() as IWord[];
  } catch (error) {
    console.error("Error adding word:", error);
  }
}
//#endregion

//#region Sentence
export async function GetSentences(word:string|undefined): Promise<ISentence[] | undefined> {
  try {
    const response = await fetch(`${textSentenceAPI}/${word}`);
      
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    return await response.json() as ISentence[];
  } catch (error) {
    console.error("Error getting sentences:", error);
  }
}
export async function GetAllSentences(): Promise<ISentence[] | undefined> {
  try {
    const response = await fetch(`${textSentenceAPI}`);
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    return await response.json() as ISentence[];
  } catch (error) {
    console.error("Error getting sentences:", error);
  }
}
//#endregion

//#region Article
export async function GetAllArticlesNames(): Promise<string[] | undefined> {
  try {
    const response = await fetch(`${textArticleAPI}/names`);
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    return await response.json() as string[];
  } catch (error) {
    console.error("Error adding word:", error);
  }
}
export async function GetArticleByName(name: string | undefined): Promise<IArticle | undefined> {
  if (name === undefined) return;
  try {
    const response = await fetch(`${textArticleAPI}/by-name/${name}`);
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    let data = await response.json() as IArticle;
    return data;
  } catch (error) {
    console.error("Error adding word:", error);
  }
}
//#endregion