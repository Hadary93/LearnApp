import React, { createContext, useContext, useState, ReactNode } from 'react';

// Define the context type
interface AppContextType {
        
    leftMenuSelected :string | undefined;
    setLeftMenuSelected: (leftMenuSelected: string|undefined) => void;

    leftMenuContent :Array<string> | undefined;
    setLeftMenuContent: (leftMenuContent: Array<string>|undefined) => void;

    selectedGroup: string;
    setSelectedGroup: (newGroup: string) => void;

    mainMenuSelected :string;
    setMainMenuSelected: (newSelection: string) => void;

    selectedWord :string | null;
    setSelectedWord: (newSelection: string|null) => void;

    isLoginOpen:boolean;
    setIsLoginOpen:React.Dispatch<React.SetStateAction<boolean>>;
}

// Create the context with a default value
export const AppContext = createContext<AppContextType | undefined>(undefined);

// Create a provider component
export const AppProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [mainMenuSelected, setMainMenuSelected] = useState<string>('');
    const [leftMenuContent, setLeftMenuContent] = useState<Array<string>|undefined>([]);
    const [leftMenuSelected, setLeftMenuSelected] = useState<string|undefined>(undefined);
    const [selectedWord, setSelectedWord] = useState<string|null>('');
    const [selectedGroup, setSelectedGroup] = useState<string>('');
    const [isLoginOpen, setIsLoginOpen] = useState(false);
    return (
        <AppContext.Provider value={{leftMenuSelected,setLeftMenuSelected, 
        leftMenuContent, setLeftMenuContent , 
        selectedWord, setSelectedWord, 
        mainMenuSelected,setMainMenuSelected, 
        selectedGroup, setSelectedGroup,
        isLoginOpen,setIsLoginOpen
        }}>
            {children}
        </AppContext.Provider>
    );
};

// Custom hook to use the context
export const useAppContext = (AppContext: React.Context<AppContextType | undefined>): AppContextType => {
    const context = useContext(AppContext);
    if (!context) {
        throw new Error('useAppContext must be used within an AppProvider');
    }
    return context;
};
