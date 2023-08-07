import os
import requests
from bs4 import BeautifulSoup
from urllib.parse import urlparse, urljoin

def download_images_from_url(url):
    response = requests.get(url)
    soup = BeautifulSoup(response.content, 'html.parser')
    img_tags = soup.find_all('img')

    image_urls = []
    for img_tag in img_tags:
        src = img_tag.get('src')
        if src:
            image_urls.append(urljoin(url, src))

    if not image_urls:
        print("No images found on the page.")
        return

    directory_name = urlparse(url).netloc.replace('.', '_')
    if not os.path.exists(directory_name):
        os.makedirs(directory_name)

    for index, image_url in enumerate(image_urls):
        try:
            response = requests.get(image_url)
            image_filename = os.path.join(directory_name, f'image_{index + 1}.jpg')
            with open(image_filename, 'wb') as f:
                f.write(response.content)
            print(f"Downloaded: {image_url}")
        except Exception as e:
            print(f"Error downloading: {image_url} - {e}")

if __name__ == "__main__":
    url = "https://ego-wear.mysapo.net/"
    download_images_from_url(url)

