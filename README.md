# MintAnimation

#### 介绍
这是一款轻量级动画插件。

#### 如何添加到项目
将以下字段添加到Packages/manifest.json中，若文件中已声明了scopedRegistries和dependencies标签，在标签中额外增加

```
{
  "scopedRegistries": [
    {
      "name": "Foldcc Tools",
      "url": "https://registry.npmjs.org",
      "scopes": [
        "com.foldcc"
      ]
    }
  ],
  "dependencies": {
    "com.foldcc.mintanimation": "0.1.5",
  }
}
```