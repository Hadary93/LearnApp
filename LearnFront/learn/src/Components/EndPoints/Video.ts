export let VideoAPI:string = "https://localhost:7015/video";
export let VideoSubtitleAPI:string = "https://localhost:7015/video/subtitle";


export async function GetVideosNames(): Promise<string[] | undefined> {
  console.log("video names calling")
  try {
    const response = await fetch(`${VideoAPI}/videos`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json"
      }
    });
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    return await response.json() as string[];
  } catch (error) {
    console.error(error);
  }
}