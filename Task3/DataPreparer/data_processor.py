import pandas as pd
import numpy as np
from sklearn.preprocessing import StandardScaler
from sklearn.decomposition import PCA


def factorize(column):
    if column.dtype in [np.float64, np.float32, np.int32, np.int64]:
        return column
    else:
        return pd.factorize(column)[0]


def normalize(dataframe):
    return StandardScaler().fit_transform(dataframe.values)


df = pd.read_csv('MallCustomers.csv', index_col=0)

df = df.apply(factorize)
pca = PCA(n_components=2)
df = pca.fit_transform(normalize(df))

pd.DataFrame(df).to_csv("./ProcessedMallCustomers.csv", index=False, header=False)