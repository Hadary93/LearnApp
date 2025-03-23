import { useEffect, useRef, useState } from "react";
import { MdFavoriteBorder } from "react-icons/md";
import { FavouriteWord } from "../EndPoints/Text";
import { Translate } from "../EndPoints/Translate";
import React from 'react';
import { voiceAPI } from "../EndPoints/Voice";
import './MediaPanel.css';
interface MediaPanelProps {
    selectedWord: string | null;
    sentenceHash: string | null;
}

export default function MediaPanel({ selectedWord,sentenceHash }: MediaPanelProps) {
    let [translatedWord, setTranslatedWord] = useState<string | undefined>();
    const voiceRef = useRef<HTMLAudioElement>(null);
    const onFavouriteClick: React.MouseEventHandler<SVGAElement> = () => {
        FavouriteWord(selectedWord)
    };
    useEffect(() => {
        const fetchTranslation = async () => {
            setTranslatedWord(await Translate(selectedWord));
        };
        fetchTranslation();
    }, [selectedWord])

    useEffect(()=>{
      if (voiceRef.current) voiceRef.current.currentTime = 0;
        voiceRef.current?.play();
    },[sentenceHash])

    return (
        <div className='translation'>
            <p style={{ gridColumn: "1" }}>{selectedWord}</p>
            <p style={{ gridColumn: "2" }}>{translatedWord}</p>
            <MdFavoriteBorder style={{ gridColumn: "3", transform: "scale(3)" }} onClick={() => onFavouriteClick} />
            <audio style={{ gridColumn: "4" }} id="audioPlayer" src={`${voiceAPI}/${sentenceHash}`} autoPlay ref={voiceRef} controls>
                Your browser does not support the audio element.
            </audio>
        </div>
    );
}