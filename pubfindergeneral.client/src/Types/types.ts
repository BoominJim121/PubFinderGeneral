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
}