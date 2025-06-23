# Etapa de build
FROM node:20 AS build
WORKDIR /app

# Copia tudo
COPY . .

# Usa o .env.production no lugar do .env
RUN mv .env.production .env

# Instala dependências
RUN npm install

# Garante que o Vite está disponível
RUN npx vite build

# Etapa final: Nginx
FROM nginx:stable-alpine
COPY --from=build /app/dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
