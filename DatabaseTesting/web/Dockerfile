# Use the official PHP image with Apache
FROM php:7.4-apache

# Install PDO MySQL driver
RUN docker-php-ext-install pdo_mysql

# Copy the contents of the 'web' directory into the container at path '/var/www/html'
COPY . /var/www/html/

# Expose port 80 for Apache
EXPOSE 80

