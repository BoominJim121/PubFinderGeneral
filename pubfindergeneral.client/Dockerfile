FROM node:13.12.0-alpine
WORKDIR /app

# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

COPY package.json ./
COPY package-lock.json ./
COPY ./ ./
RUN apk add --update python make g++\
   && rm -rf /var/cache/apk/*
RUN npm i
CMD ["npm", "run", "start"]