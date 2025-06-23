# Etapa de build
FROM node:20 AS build
WORKDIR /app

# Copia tudo
COPY . .

# Cria o .env com a URL da API em produção
RUN echo "VITE_API_URL=https://..." > .env && npm install && npx vite build

# Instala dependências
RUN npm install

# Garante que o Vite está disponível e realiza o build
RUN npx vite build

# Etapa final: Nginx
FROM nginx:stable-alpine
COPY --from=build /app/dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
