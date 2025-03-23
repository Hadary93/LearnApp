import { Card } from 'antd';
import './Sentences.css';
import { useEffect, useRef, useState } from 'react';

export default function Sentences() {
    const corectSentence = ["Guten", "Tag"]
    const schuffledSentence = ["Tag", "Guten"]
    const sentenceRef = useRef<HTMLParagraphElement>(null);
    const [constructedSentence, setConstuctedSetnence] = useState<string[]>([]);
    const [selectedCards, setSelectedCards] = useState<Array<HTMLElement | undefined | null>>([]);

    const onCardClick = (word: string, cardElement: HTMLElement | undefined | null): void => {
        if (!selectedCards.includes(cardElement)) {
            setSelectedCards((prev) => [...prev, cardElement])
            setConstuctedSetnence((prev) => [...prev, word])
        } else {
            setSelectedCards((prev) => [...prev.filter(x => x != cardElement)])
            setConstuctedSetnence((prev) => [...prev.filter(x => x != word)])
            cardElement?.classList.remove(cardElement?.classList[cardElement?.classList.length - 1])
        }
    };
    useEffect(() => {
        selectedCards.map((c) => {
            c?.classList.add('ant-card-selected')
        })
    }, [selectedCards])
    useEffect(() => {
        if (constructedSentence[constructedSentence.length - 1] === corectSentence[constructedSentence.length - 1]) {
            sentenceRef.current?.classList.add('sentence-correct')
        }else{
            sentenceRef.current?.classList.remove('sentence-correct')
        }
    }, [constructedSentence])
    return (
        <div className='sentences'>
            <div style={{ display: "flex", flexDirection: "column", height: "80%", justifySelf: "left", overflow: "auto", }}>
                {schuffledSentence.map((x) => <Card
                    key={x}
                    hoverable
                    style={{ width: 240, margin: 25, gridColumn: 1, justifyItems: "center" }}
                    onClick={(event) => onCardClick(x, event?.currentTarget)}>
                    <p>{x}</p>
                </Card>)}
            </div>
            <div style={{ display: "flex", justifySelf: "left", fontWeight:"bold", fontSize:50 }}>
                <p ref={sentenceRef}>
                    {constructedSentence.map((x) => <span
                        key={x}
                        style={{ width: 240, justifyItems: "center" }}
                        onClick={(event) => onCardClick(x, event?.currentTarget)}>
                        {x}{" "}
                    </span>)}
                </p>

            </div>

        </div>
    );
}

