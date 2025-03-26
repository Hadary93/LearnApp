import { Card } from 'antd';
import './Words.css';
import { useEffect, useState } from 'react';
import { GetFavourites } from '../../EndPoints/Text';

export default function Words() {
    const [German, setGerman] = useState<(string)[]>([]);
    const [English, setEnglish] = useState<string[] >([]);
    const [shiffledEnglish, setshuffledEnglish] = useState<string[] >([]);
    const [selectedCards, setSelectedCards] = useState<Array<HTMLElement | undefined | null>>([]);
    const [germanIndex, setGermanIndex] = useState<number | null>(null);
    const [englishIndex, setEnglishIndex] = useState<number | null>(null);

    function shuffleArray(array: string[]): string[] {
        let shuffled = [...array]; // Create a copy to avoid mutating the original array
        for (let i = shuffled.length - 1; i > 0; i--) {
            const j = Math.floor(Math.random() * (i + 1)); // Get a random index
            [shuffled[i], shuffled[j]] = [shuffled[j], shuffled[i]]; // Swap elements
        }
        return shuffled;
    }
    
    useEffect(()=>{
        const favourites = async()=>{
            return await GetFavourites();
        }
        favourites().then((data)=>{
            console.log(data)
            if(data){
                setGerman(data.map(x=>x.wordText??''))
                setEnglish(data.map(x=>x.translation??''))
                setshuffledEnglish(shuffleArray(data.map(x=>x.translation??'')))
            }
          
        });
    },[])
    useEffect(() => { 
        if(germanIndex!=null && germanIndex === englishIndex){
            console.log("match")
            selectedCards.map((c)=>{
                c?.classList.remove('ant-card-selected')
                c?.classList.add('ant-card-selected-true')
            })
        }
    }, 
    [germanIndex,englishIndex])


    useEffect(()=>{
        selectedCards.map((c)=>{
            c?.classList.add('ant-card-selected')
        })
    },[selectedCards])

    const onCardClick = (word: string,lang: string, cardElement: HTMLElement | undefined | null): void => {
        if(lang =="german")  {setGermanIndex(German.indexOf(word))}
        if(lang =="english")  {setEnglishIndex(English.indexOf(word))}

        setSelectedCards((prev) => {
            if(prev.indexOf(cardElement)==-1){
                return [...prev, cardElement]
            }else{
                cardElement?.classList.remove('ant-card-selected')
                if(lang =="german")  {setGermanIndex(null)}
                if(lang =="english")  {setEnglishIndex(null)}
                return prev.filter((x=>x!=cardElement))
            }
        } );
    };
    return (
        <div className='words'>
            {German.map((x, i) => <Card
                key={i}
                hoverable
                style={{ width: 240, margin: 10, gridColumn: 1, gridRow: i + 1, justifyItems: "center" }}
                onClick={(event) => onCardClick(x,"german", event?.currentTarget)}>
                <p>{x}</p>
            </Card>)}
            {shiffledEnglish.map((x, i) => <Card
                key={i}
                hoverable
                style={{ width: 240, margin: 10, gridColumn: 2, gridRow: i + 1, justifyItems: "center" }}
                onClick={(event) => onCardClick(x, "english",event?.currentTarget)}>
                <p>{x}</p>
            </Card>)}
        </div>
    );
}

