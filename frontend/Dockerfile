# Build da aplicação
FROM node:20 AS build
WORKDIR /app

COPY . .

# Garante que o Vite use o .env.production
ENV NODE_ENV=production

RUN npm install
RUN npm run build

# Servidor Nginx
FROM nginx:stable-alpine
COPY --from=build /app/dist /usr/share/nginx/html

# Copia o arquivo de configuração do nginx (opcional)
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
