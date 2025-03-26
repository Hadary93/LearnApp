const translateAPI = "https://localhost:7283/translate"


export async function DeepLAPIKey(key:string | null): Promise<boolean| undefined> {
  try {
    const response = await fetch(`${translateAPI}/api-key/${key}`, {
      method: "POST"
    });
    console.log(response)
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    return await response.ok;
  } catch (error) {
    console.error(error);
  }
}
export async function Translate(word: string | null): Promise<string | undefined> {
    try {
        const response = await fetch(`${translateAPI}/text/${word}`)
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        const translation: string = await response.text();
        return translation;
    } catch (error) {
        console.error("Error fetching translation:", error);
    }
}