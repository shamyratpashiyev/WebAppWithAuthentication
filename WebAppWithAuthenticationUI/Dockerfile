FROM node:22-alpine AS build
WORKDIR /app

COPY package*.json ./
RUN npm ci

COPY . ./
RUN npm run build -- --configuration production

FROM node:22-alpine AS final
WORKDIR /app

RUN npm install -g serve@14.2.4
COPY --from=build /app/dist ./dist

# Render provides PORT at runtime; 10000 is a local fallback.
CMD ["sh", "-c", "INDEX_FILE=$(find /app/dist -maxdepth 4 -type f -name index.html | head -n1); [ -n \"$INDEX_FILE\" ] || { echo 'index.html not found under /app/dist'; exit 1; }; APP_DIR=$(dirname \"$INDEX_FILE\"); serve -s \"$APP_DIR\" -l ${PORT:-10000}"]

