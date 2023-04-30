import time
from selenium.webdriver.common.by import By
from selenium.webdriver.chrome.options import Options as ChromeOptions
from selenium.webdriver.chrome.service import Service as ChromeService
from selenium import webdriver

service = ChromeService(executable_path="D:/Programozas/Selenium Webdriver/chromedriver", port=6666) #at this line you can use your ChromeDriverManager
options = ChromeOptions()
options.add_argument("--start-maximized")
driver = webdriver.Chrome(service=service, options=options)

driver.get('http://localhost:4200/home')

driver.switch_to.window(driver.current_window_handle)
time.sleep(1.5)

shopNavLink = driver.find_element(By.ID, "selenium__shop")
shopNavLink.click()
time.sleep(2)

statisticsNavLink = driver.find_element(By.ID, "selenium__statistics")
statisticsNavLink.click()
time.sleep(2)

loginNavLink = driver.find_element(By.ID, "selenium__login")
loginNavLink.click()
time.sleep(2)


# driver.execute_script("window.scrollTo(0, document.body.scrollHeight);")



# kereso = bongeszo.find_element_by_id("id-search-field")
# kereso.send_keys("documentation")
# kereso.send_keys(Keys.ENTER)

# eredmeny = bongeszo.find_element_by_xpath('//*[@id="touchnav-wrapper"]/header/div/div[2]/div/p[4]/a')
# eredmeny.click()
