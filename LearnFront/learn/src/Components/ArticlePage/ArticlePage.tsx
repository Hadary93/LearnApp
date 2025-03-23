import './ArticlePage.css';
import React, { useEffect, useState } from 'react';
import { AppContext, useAppContext } from '../AppContext';
import { AiOutlineSound } from "react-icons/ai";
import { IArticle } from '../Interfaces/Interfaces';
import { FavouriteWord, GetAllArticlesNames, GetArticleByName } from '../EndPoints/Text';
import MediaPanel from '../Custom/MediaPanel';
import LeftMenu from '../LeftMenu/LeftMenu';

export default function ArticlePage() {
    const { leftMenuSelected, selectedWord, setSelectedWord,setLeftMenuContent } = useAppContext(AppContext);
    let [article, setArticle] = useState<IArticle | null>(null);
    let [sentenceHash, setSentenceHash] = useState<string | null>(null);
    
    useEffect(() => {
        const articleNames = async () => {
            setLeftMenuContent(await GetAllArticlesNames());
        }
        articleNames()
    }, [])
    useEffect(() => {
        async function getArticle() {
            var currentArticle = await GetArticleByName(leftMenuSelected ?? undefined);
            if (currentArticle) { setArticle(currentArticle) } else { setArticle(null) }
        }
        getArticle();
    }, [leftMenuSelected])

    useEffect(() => {
        console.log(article?.paragraphs)
    }, [article])

    useEffect(() => {
        const fetchTranslation = async () => {
            if (selectedWord) {
                //const result = await Translate(selectedWord);
                FavouriteWord(selectedWord);
            }
        };
        fetchTranslation();
    }, [selectedWord])

    const onWordClick: React.MouseEventHandler<HTMLSpanElement> = (event) => {
        setSelectedWord(event.currentTarget.innerHTML.trim())
    };

    const onSentenceClick: React.MouseEventHandler<HTMLSpanElement> = () => {
    };

    const onSentenceVoiceClick: React.MouseEventHandler<SVGAElement> = (event) => {
        setSentenceHash(event.currentTarget.getAttribute("sentence-key"))
        console.log(event.currentTarget.getAttribute("sentence-key"))
    };
    return (
        <>
            <div className='article-page' >

                <h1 style={{margin:10}}>{article?.name}</h1>
                <article style={{ width: "100%", height: "100%", overflow: "auto" }}>
                    {article !== null &&
                        article.paragraphs.map((paragraph, pIndex) => (
                            <p key={pIndex}>
                                {paragraph.sentences.map((sentence, sIndex) => (
                                    <span key={sIndex} className='sentence' onClick={onSentenceClick}>

                                        {sentence.words?.map((word, wIndex) => (
                                            <span key={wIndex} className='word' onClick={onWordClick}>
                                                {" "}
                                                {/* Add a space before the word unless it's punctuation */}
                                                {wIndex > 0 && ![".", ","].includes(word.wordText ?? '') && " "}
                                                {word.wordText}

                                            </span>
                                        ))}

                                        <AiOutlineSound key={sIndex} sentence-key={sentence.hash} className='sentencePlayIcon' onClick={onSentenceVoiceClick} />
                                    </span>
                                ))}
                            </p>
                        ))}
                </article>
            </div>
            <MediaPanel sentenceHash={sentenceHash} selectedWord={selectedWord} />
            <LeftMenu></LeftMenu>
        </>
    );
}
