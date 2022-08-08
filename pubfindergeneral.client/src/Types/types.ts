export interface IPublicHouseResponse {
    pubs: IPublicHouse[],
    totalPages: number
}
export interface IPubFinderGeneralDataParams {
    pageSize: number,
    pageNumber: number
}
export interface IPubCardListProps {
    publicHouses: IPublicHouseResponse 
}
export interface IPublicHouse {
    name: string,
    category?: string,
    lastUpdatedDateTime?: string,
    excerpt?: string,
    about?: IAbout
}
export interface IAbout {
    website?: string,
    thumbnail?: string,
    locale?: ILocale,
    ratings?: IRating[],
    tags?: string[],
    twitter?: string
}
export interface ILocale {
    address?: string,
    latitude?: number,
    longitude?: number
}
export interface IRating {
    name?: string,
    value?: number
}
export interface IPubs {
    PubJsonList: IPublicHouse[] | undefined;
}

export interface IPubCardProps {
    Name: string,
    Excerpt: string,
    Thumbnail: string,
    AboutValue: IAbout | undefined
}

export interface IAboutProps {
    open: boolean;
    selectedValue: IAbout| undefined;
    name: string;
    onClose: (value: IAbout| undefined) => void;
}
