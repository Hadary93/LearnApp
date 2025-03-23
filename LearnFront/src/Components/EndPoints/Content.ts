export let contentAPI:string = "https://localhost:7245/content";
export let contentUploadAPI:string = 'https://localhost:7245/content/upload-content';

export async function ProcessContent(fileNames: string [] | null): Promise<boolean | undefined> {
  try {
      const queryParams = fileNames?.map(name => `fileNames=${encodeURIComponent(name)}`).join("&");
      const response = await fetch(`${contentAPI}/process-content?${queryParams}`,{
        method:"post"
      })
      if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
      }
      return true;
  } catch (error) {
      console.error("Error processing the content:", error);
      return false;
  }
}